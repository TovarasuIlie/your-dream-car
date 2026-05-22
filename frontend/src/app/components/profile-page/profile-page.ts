import { CommonModule } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { LoggedUser, UserProfile } from '../../models/logged-user';
import { QuizTableItem } from '../../models/quiz';
import { AuthService } from '../../services/auth-service';
import { QuizService } from '../../services/quiz-service';
import { single } from 'rxjs';
import { ViewResponseModal } from "../ui/view-response-modal/view-response-modal";

@Component({
	selector: 'app-profile-page',
	imports: [CommonModule, RouterLink, ViewResponseModal],
	templateUrl: './profile-page.html',
	styleUrl: './profile-page.css',
})
export class ProfilePage {
	authService = inject(AuthService);
	quizService = inject(QuizService);

	user = signal<UserProfile | null>(null);

	quizzes = signal<QuizTableItem[]>([]);
	isLoading = signal<boolean>(true);

	isQuizModalOpen = false;
	quizIdSelected = signal<number | null>(null);

	ngOnInit() {
		this.loadProfileData();
	}

	loadProfileData() {
		this.isLoading.set(true);

		this.authService.user$.subscribe(value => {
			if (value) {
				this.user.set({
					name: value.name,
					email: value.email
				});
			}
		})

		this.quizService.getYourQuizzes().subscribe(quizzes => {
			this.quizzes.set(quizzes);
		})

		this.isLoading.set(false);
	}

	get resolvedCount(): number {
		return this.quizzes().filter(q => !q.isActive).length;
	}

	handleCloseQuizModal() {
		this.isQuizModalOpen = false;
		this.quizIdSelected.set(null);
	}

	handleOpenQuizModal(quizId: number) {
		this.quizIdSelected.set(quizId);
		this.isQuizModalOpen = true;
	}
}
