import { CommonModule } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { QuizDetail } from '../../models/quiz';
import { QuizService } from '../../services/quiz-service';
import { ToastService } from '../../services/toast-service';

@Component({
	selector: 'app-view-quiz-page',
	imports: [CommonModule, RouterLink],
	templateUrl: './view-quiz-page.html',
	styleUrl: './view-quiz-page.css',
})
export class ViewQuizPage {
	private route = inject(ActivatedRoute);
	quizService = inject(QuizService);
	toastService = inject(ToastService);

	quiz = signal<QuizDetail | null>(null);
	isLoading = signal<boolean>(true);
	isResolving = signal<boolean>(false);

	ngOnInit() {
		const idParam = this.route.snapshot.paramMap.get('id');

		if (idParam) {
			const id = Number(idParam);
			if (!isNaN(id)) {
				this.loadQuizDetails(id);
			}
		}
	}

	loadQuizDetails(id: number) {
		this.isLoading.set(true);

		this.quizService.getQuizResponses(id).subscribe(quiz => {
			this.quiz.set(quiz);
		})

		this.isLoading.set(false);
	}

	markAsResolved() {
		this.isResolving.set(true);

		this.quizService.toggleQuizStatus(this.quiz()?.id || 0).subscribe({
			next: (value: any) => {
				this.quiz.update(current => {
					if (!current) return current;
					return { ...current, isActive: false };
				});
				this.toastService.show(value.message, 'success');
			},
			error: (err) => {
				this.toastService.show(err.error.message, 'error');
			}
		})

		this.isResolving.set(false);
	}
}
