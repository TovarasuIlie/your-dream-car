import { CommonModule } from '@angular/common';
import { Component, inject, OnInit, signal } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AlertComponent } from "../ui/alert/alert.component";
import { ThemeService } from '../../services/theme-service';
import { AuthService } from '../../services/auth-service';
import { ToastService } from '../../services/toast-service';

@Component({
	selector: 'app-register-page',
	imports: [CommonModule, ReactiveFormsModule, RouterModule, AlertComponent],
	templateUrl: './register-page.html',
	styleUrl: './register-page.css',
})
export class RegisterPage implements OnInit {
	private fb: FormBuilder = inject(FormBuilder);
	themeService = inject(ThemeService);
	authService = inject(AuthService);
	router = inject(Router);
	toastService = inject(ToastService);

	registerForm: FormGroup = new FormGroup([]);

	showPassword = signal<boolean>(false);
	showConfirmPassword = signal<boolean>(false);
	isLoading = signal<boolean>(false);
	errorMessage =  signal<string | null>(null);

	ngOnInit(): void {
		this.registerForm = this.fb.group({
			name: ['', [Validators.required, Validators.minLength(3)]],
			email: ['', [Validators.required, Validators.email]],
			password: ['', [Validators.required, Validators.minLength(6)]],
			confirmPassword: ['', [Validators.required]]
		}, { validators: this.passwordMatchValidator });
	}

	passwordMatchValidator(control: AbstractControl): ValidationErrors | null {
		const password = control.get('password')?.value;
		const confirmPassword = control.get('confirmPassword')?.value;

		if (password !== confirmPassword) {
			control.get('confirmPassword')?.setErrors({ passwordMismatch: true });
			return { passwordMismatch: true };
		} else {
			return null;
		}
	}

	togglePasswordVisibility(field: 'password' | 'confirm') {
		if (field === 'password') {
			this.showPassword.update(val => !val);
		} else {
			this.showConfirmPassword.update(val => !val);
		}
	}

	get f() { return this.registerForm.controls; }

	onSubmit() {
		if (this.registerForm.invalid) {
			this.registerForm.markAllAsTouched();
			return;
		}

		this.isLoading.set(true);
		this.errorMessage.set(null);

		this.authService.registerAccount(this.registerForm.value).subscribe({
			next: () => {
				this.toastService.show("Contul a fost creat cu succes!", 'success');
				this.router.navigateByUrl("/");
			},
			error: (err) => {
				this.errorMessage.set(err.error.message);
			}
		})

		this.isLoading.set(false);
	}
}
