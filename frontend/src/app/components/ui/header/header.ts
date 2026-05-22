import { Component, ElementRef, HostListener, inject, OnInit, signal } from '@angular/core';
import { ThemeService } from '../../../services/theme-service';
import { LoginModal } from "../login-modal/login-modal";
import { AuthService } from '../../../services/auth-service';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { LoggedUser } from '../../../models/logged-user';
import { ToastService } from '../../../services/toast-service';
import { UserRole } from '../../../enums/user-role';

@Component({
	selector: 'app-header',
	imports: [CommonModule, RouterLink, LoginModal],
	templateUrl: './header.html',
	styleUrl: './header.css',
})
export class Header implements OnInit {
	themeService = inject(ThemeService);
	authService = inject(AuthService);
	toastService = inject(ToastService);

	isLoginModalOpen = false;
	isDropdownOpen = signal<boolean>(false);
	loggedUser?: LoggedUser;
	private eRef = inject(ElementRef);

	ngOnInit(): void {
		this.authService.user$.subscribe(value => {
			if (value) {
				this.loggedUser = value;
			}
		})
	}

	isAdmin() {
		return this.authService.hasUserRole(UserRole.Admin)
	}

	openLoginModal() {
		this.isLoginModalOpen = true;
	}

	closeLoginModal() {
		this.isLoginModalOpen = false;
	}

	toggleDropdown() {
		this.isDropdownOpen.update(val => !val);
	}

	closeDropdown() {
		this.isDropdownOpen.set(false);
	}

	logout() {
		this.authService.logout();
		this.loggedUser = undefined;
		this.isDropdownOpen.set(false);
		this.toastService.show("Te-ai delogat cu succes!", 'success');
	}

	@HostListener('document:click', ['$event'])
    onClickOutside(event: MouseEvent) {
        if (this.isDropdownOpen() && !this.eRef.nativeElement.contains(event.target)) {
            this.closeDropdown();
        }
    }
}
