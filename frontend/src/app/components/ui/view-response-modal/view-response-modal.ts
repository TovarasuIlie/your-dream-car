import { Component, EventEmitter, inject, Input, OnChanges, Output, signal, SimpleChanges } from '@angular/core';
import { QuizModalData } from '../../../models/quiz';
import { QuizService } from '../../../services/quiz-service';
import { CommonModule } from '@angular/common';
import { map } from 'rxjs';

@Component({
	selector: 'app-view-response-modal',
	imports: [CommonModule],
	templateUrl: './view-response-modal.html',
	styleUrl: './view-response-modal.css',
})
export class ViewResponseModal implements OnChanges {
	quizService = inject(QuizService);

	@Input() isOpen: boolean = false;
	@Input() quizId: number | null = null;

	@Output() close = new EventEmitter<void>();

	quiz = signal<QuizModalData | null>(null);

	ngOnChanges(changes: SimpleChanges): void {
		if (this.quizId && (changes['quizId'] || changes['isOpen']?.currentValue === true)) {
			this.quiz.set(null);
			this.quizService.getQuizResponses(this.quizId)
				.pipe(
					map((value: any) => {
						return {
							id: value.id,
							createdAt: new Date(value.createdAt),
							answers: value.answers
						} as QuizModalData;
					})
				)
				.subscribe({
                    next: (value: QuizModalData) => {
                        this.quiz.set(value);
                    },
                    error: (err) => {
                        console.error('Eroare API:', err);
                    }
                });
		}
	}

	closeModal() {
		this.close.emit();
	}
}
