import { CommonModule } from '@angular/common';
import { Component, EventEmitter, inject, Input, OnInit, Output, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AlertComponent } from "../alert/alert.component";
import { RouterLink } from '@angular/router';
import { AuthService } from '../../../services/auth-service';
import { ToastService } from '../../../services/toast-service';

@Component({
	selector: 'app-login-modal',
	imports: [CommonModule, ReactiveFormsModule, AlertComponent, RouterLink],
	templateUrl: './login-modal.html',
	styleUrl: './login-modal.css',
})
export class LoginModal implements OnInit {
	private fb: FormBuilder = inject(FormBuilder);
	authService = inject(AuthService);
	toastService = inject(ToastService);

	@Input() isLoginModalOpen: boolean = false;
	@Output() close = new EventEmitter<void>();

	loginForm: FormGroup = new FormGroup([]);
	showPassword = signal<boolean>(false);
	isLoading = signal<boolean>(false);
	errorMessage = signal<string | null>(null);

	ngOnInit(): void {
		this.loginForm = this.fb.group({
			email: ['', [Validators.required, Validators.email]],
			password: ['', [Validators.required]]
		});
	}

	get f() { return this.loginForm.controls; }

	togglePasswordVisibility() {
		this.showPassword.update(val => !val);
	}

	closeModal() {
		this.close.emit();
	}

	onSubmit() {
		if (this.loginForm.invalid) {
			this.loginForm.markAllAsTouched();
			return;
		}

		this.isLoading.set(true);
		this.errorMessage.set(null);
		
		this.authService.loginAccount(this.loginForm.value).subscribe({
			next: () => {
				this.toastService.show("Te-ai autentificat cu succes!", 'success');
				this.close.emit();
			},
			error: (err) => {
				this.errorMessage.set(err.error.message);
			}
		})

		this.isLoading.set(false);
	}
}