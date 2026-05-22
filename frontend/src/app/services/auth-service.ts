import { HttpClient } from '@angular/common/http';
import { inject, Injectable, OnInit } from '@angular/core';
import { LoginForm, RegisterForm } from '../models/auth-form';
import { environment } from '../../environments/environment.development';
import { LoggedUser } from '../models/logged-user';
import { Router } from '@angular/router';
import { map, ReplaySubject } from 'rxjs';
import { jwtDecode } from 'jwt-decode';
import { UserRole } from '../enums/user-role';

@Injectable({
	providedIn: 'root',
})
export class AuthService {
	private http = inject(HttpClient);
	private router = inject(Router);

	private userSource = new ReplaySubject<LoggedUser | null>(1);
	user$ = this.userSource.asObservable();

	constructor() {
		this.loadUserFromLocalStorage();
	}

	registerAccount(registerForm: RegisterForm) {
		return this.http.post(environment.API_URL + "api/Auth/register-user", registerForm);
	}

	loginAccount(loginForm: LoginForm) {
		return this.http.post<LoggedUser>(environment.API_URL + "api/Auth/login-user", loginForm).pipe(
			map((user: LoggedUser) => {
				if (user) {
					this.setUser(user);
				}
			})
		);
	}

	logout() {
		localStorage.removeItem(environment.USER_KEY);
		this.userSource.next(null);
		this.router.navigateByUrl('/');
	}

	private loadUserFromLocalStorage() {
		const userString = localStorage.getItem(environment.USER_KEY);

		if(userString) {
			try {
				const user: LoggedUser = JSON.parse(userString);
				
				if (this.isTokenValid()) {
					this.userSource.next(user);
				} else {
					this.logout();
				}
			} catch (e) {
				this.logout();
			}
		} else {
			this.userSource.next(null);
		}
	}

	private setUser(user: LoggedUser) {
		localStorage.setItem(environment.USER_KEY, JSON.stringify(user));
		this.userSource.next(user);
	}

	getToken() {
		const storedData = localStorage.getItem(environment.USER_KEY);
		const user: LoggedUser | null = storedData ? JSON.parse(storedData) : null;

		if (!user) return null;

		return user.token;
	}

	isTokenValid() {
		const token = this.getToken();

		if (!token) return false;

		try {
			const decoded: any = jwtDecode(token);
			const isTokenExpired = Date.now() >= decoded.exp * 1000;
			return !isTokenExpired;
		} catch (e) {
			return false;
		}
	}

	getUserRole() {
		const storedData = localStorage.getItem(environment.USER_KEY);
		const user: LoggedUser | null = storedData ? JSON.parse(storedData) : null;

		if (!user) return null;

		return user.role;
	}

	hasUserRole(userRole: UserRole) {
		const role = this.getUserRole();

		if (!role) return false;

		return role === userRole;
	}
}
