export interface QuizOption {
    id: number;
    label: string;
    description?: string;
}

export interface QuizQuestion {
    id: number;
    title: string;
    subtitle: string;
    options: QuizOption[];
    weight: number;
}

export interface Answer {
    question: number;
    options: number[];
    weight: number;
}

export interface QuizTableItem {
    id: number;
    name: string;
    email: string;
    createdAt: Date;
    isActive: boolean;
}

export interface QuizDetail {
    id: number;
    name: string;
    email: string;
    createdAt: Date;
    isActive: boolean;
    answers: { question: string; responses: string[]; weight: number }[];
}

export interface QuizModalData {
  id: number;
  createdAt: Date;
  answers: { question: string; responses: string[]; weight: number }[];
}