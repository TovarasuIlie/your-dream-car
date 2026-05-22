import { Component, inject, signal } from '@angular/core';
import { QuizTableItem } from '../../models/quiz';
import { CommonModule } from '@angular/common';
import { RouterLink } from "@angular/router";
import { QuizService } from '../../services/quiz-service';

@Component({
	selector: 'app-quizzies-page',
	imports: [CommonModule, RouterLink],
	templateUrl: './quizzies-page.html',
	styleUrl: './quizzies-page.css',
})
export class QuizziesPage {
	quizService = inject(QuizService);

	quizzes = signal<QuizTableItem[]>([]);
	isLoading = signal<boolean>(true);

	ngOnInit() {
		this.loadQuizzes();
	}

	loadQuizzes() {
		this.isLoading.set(true);

		this.quizService.getAllQuizzes().subscribe(quizzes => {
			this.quizzes.set(quizzes);
		})
		
		this.isLoading.set(false);
	}

	getStatusClass(isActive: boolean): string {
		if (isActive) {
			return 'bg-green-100 text-green-700 dark:bg-green-900/30 dark:text-green-400';
		} else {
			return 'bg-slate-100 text-slate-700 dark:bg-slate-800 dark:text-slate-400';
		}
	}

	getStatusLabel(isActive: boolean): string {
		return isActive ? 'Activ' : 'Rezolvat';
	}
}
