import { CommonModule } from '@angular/common';
import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { Answer, QuizQuestion } from '../../models/quiz';
import { QuizService } from '../../services/quiz-service';
import { map } from 'rxjs';
import { ToastService } from '../../services/toast-service';

@Component({
	selector: 'app-quiz-page',
	imports: [CommonModule, RouterModule],
	templateUrl: './quiz-page.html',
	styleUrl: './quiz-page.css',
})
export class QuizPage implements OnInit {
	quizService = inject(QuizService);
	toastService = inject(ToastService);
	router = inject(Router);

	questions = signal<QuizQuestion[]>([]);
	currentStep = signal<number>(0);
	answers = signal<Record<number, Answer>>({});

	ngOnInit(): void {
		this.quizService.getAllQuestions()
			.pipe(
				map(data => data.map(q => ({ ...q, weight: 3 })))
			)
			.subscribe(questions => {
				this.questions.set(questions);
			})
	}

	currentQuestion = computed(() => this.questions()[this.currentStep()]);

	isLastStep = computed(() => this.currentStep() === this.questions().length - 1);

	progressPercentage = computed(() => {
		const total = this.questions().length;
		const answeredCount = Object.keys(this.answers()).length;
		return Math.round((answeredCount / total) * 100);
	});

	hasAnsweredCurrent = computed(() => {
		const qId = this.currentQuestion()?.id;
		return qId ? !!this.answers()[qId]?.options?.length : false;
	});

	selectOption(questionId: number, optionId: number) {
		this.answers.update(current => {
			const existingAnswer = current[questionId];
			const existingWeight = existingAnswer?.weight || 3;
			let newOptionIds = existingAnswer?.options || [];

			if (newOptionIds.includes(optionId)) {
				newOptionIds = newOptionIds.filter(id => id !== optionId);
			} else {
				newOptionIds = [...newOptionIds, optionId];
			}

			if (newOptionIds.length === 0) {
				const newState = { ...current };
				delete newState[questionId];
				return newState;
			}

			return {
				...current,
				[questionId]: {
					question: questionId,
					options: newOptionIds,
					weight: existingWeight
				}
			};
		});
	}

	setWeight(questionId: number, weight: number) {
		this.answers.update(current => {
			if (!current[questionId]) return current;

			return {
				...current,
				[questionId]: {
					...current[questionId],
					weight: weight
				}
			};
		});
	}

	isOptionSelected(questionId: number, optionId: number): boolean {
		return this.answers()[questionId]?.options?.includes(optionId) || false;
	}

	getWeightLabel(weight: number): string {
		switch (weight) {
			case 1: return "Nu contează prea mult";
			case 2: return "Preferință ușoară";
			case 3: return "Important, dar sunt flexibil";
			case 4: return "Foarte important";
			case 5: return "Esențial (Deal-breaker)";
			default: return "";
		}
	}

	prevStep() {
		if (this.currentStep() > 0) {
			this.currentStep.update(step => step - 1);
		}
	}

	nextStep() {
		if (!this.isLastStep()) {
			this.currentStep.update(step => step + 1);
		}
	}

	submitQuiz() {
		this.quizService.saveQuiz(Object.values(this.answers())).subscribe({
			next: (value: any) => {
				this.toastService.show(value.message, 'success');
				this.router.navigateByUrl('/');
			},
			error: (err) => {
				this.toastService.show(err.error.message, 'error');
			}
		})
	}
}
