import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { Answer, QuizDetail, QuizQuestion, QuizTableItem } from '../models/quiz';

@Injectable({
	providedIn: 'root',
})
export class QuizService {

	private http = inject(HttpClient);

	getAllQuestions() {
		return this.http.get<QuizQuestion[]>(environment.API_URL + "api/Quiz/get-quiz-questions");
	}

	saveQuiz(answers: Answer[]) {
		return this.http.post(environment.API_URL + "api/Quiz/save-new-quiz", answers);
	}

	getAllQuizzes() {
		return this.http.get<QuizTableItem[]>(environment.API_URL + "api/Quiz/get-all-quizzes");
	}

	getYourQuizzes() {
		return this.http.get<QuizTableItem[]>(environment.API_URL + "api/Quiz/get-your-quizzes");
	}

	getQuizResponses(quizId: number) {
		return this.http.get<QuizDetail>(environment.API_URL + "api/Quiz/get-quiz/" + quizId);
	}

	toggleQuizStatus(quizId: number) {
		return this.http.patch(environment.API_URL + "api/Quiz/toggle-quiz-status/" + quizId, {});
	}
}
