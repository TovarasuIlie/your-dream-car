import { Routes } from '@angular/router';
import { IndexPage } from './components/index-page/index-page';
import { QuizPage } from './components/quiz-page/quiz-page';
import { RegisterPage } from './components/register-page/register-page';
import { PageLayout } from './components/ui/page-layout/page-layout';
import { authGuard } from './guards/auth-guard';
import { guestGuard } from './guards/guest-guard';
import { QuizziesPage } from './components/quizzies-page/quizzies-page';
import { ViewQuizPage } from './components/view-quiz-page/view-quiz-page';
import { ProfilePage } from './components/profile-page/profile-page';

export const routes: Routes = [
    {
        path: '',
        component: PageLayout,
        children: [
            {
                path: '',
                component: IndexPage
            },
            {
                path: 'chestionar',
                component: QuizPage,
                canActivate: [authGuard]
            },
            {
                path: 'profil',
                component: ProfilePage,
                canActivate: [authGuard]
            },
            {
                path: 'dashboard',
                component: QuizziesPage,
                canActivate: [authGuard]
            },
            {
                path: 'dashboard/vezi-chestionar/:id',
                component: ViewQuizPage,
                canActivate: [authGuard]
            }
        ]
    },
    {
        path: 'inregistrare-cont',
        component: RegisterPage,
        canActivate: [guestGuard]
    }
];
