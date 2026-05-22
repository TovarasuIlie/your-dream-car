import { Component, computed, inject, signal } from '@angular/core';
import { Header } from "../ui/header/header";
import { Footer } from "../ui/footer/footer";
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

interface Brand {
	id: string;
	name: string;
	logoUrl: string;
}

interface CarSpec {
	name: string;
	textValue: string;
	score: number;
}

interface PopularCar {
	id: string;
	rank: number;
	name: string;
	imageUrl: string;
	rating: number;
	reviewCount: number;
	specs: CarSpec[];
}

@Component({
	selector: 'app-index-page',
	imports: [CommonModule, RouterLink],
	templateUrl: './index-page.html',
	styleUrl: './index-page.css',
})
export class IndexPage {
	availableBrands = signal<Brand[]>([
		{ id: 'bmw', name: 'BMW', logoUrl: 'https://upload.wikimedia.org/wikipedia/commons/4/44/BMW.svg' },
		{ id: 'audi', name: 'Audi', logoUrl: 'https://upload.wikimedia.org/wikipedia/commons/9/92/Audi-Logo_2016.svg' },
		{ id: 'mercedes', name: 'Mercedes-Benz', logoUrl: 'https://upload.wikimedia.org/wikipedia/commons/9/90/Mercedes-Logo.svg' }
	]);

	popularModels = signal<PopularCar[]>([
		{
			id: 'm1',
			rank: 1,
			name: 'BMW M3 Competition',
			imageUrl: 'https://hips.hearstapps.com/hmg-prod/images/2021-bmw-m3-competition-144-1628778080.jpg',
			rating: 4.8,
			reviewCount: 342,
			specs: [
				{ name: 'Putere', textValue: '510 CP', score: 90 },
				{ name: 'Accelerație', textValue: '3.9s (0-100)', score: 85 },
				{ name: 'Manevrabilitate', textValue: 'M xDrive', score: 95 },
				{ name: 'Eficiență', textValue: '10.2 l/100km', score: 40 }
			]
		},
{
            id: 'm2',
            rank: 2,
            name: 'Mercedes-Benz AMG GT',
            imageUrl: 'https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?w=800&q=80',
            rating: 4.8,
            reviewCount: 320,
            specs: [
                { name: 'Performanță', textValue: '367 CP', score: 85},
                { name: 'Accelerație', textValue: '4.8s (0-100)', score: 75 },
                { name: 'Confort', textValue: 'Piele Nappa & MBUX', score: 98},
                { name: 'Consum Mixt', textValue: '8.3 L/100km', score: 65 }
            ]
        },
		{
			id: 'm3',
			rank: 3,
			name: 'Audi RS6 Avant',
			imageUrl: 'https://hips.hearstapps.com/hmg-prod/images/2023-audi-rs-6-avant-performance111-6499d2d01c180.jpg?crop=0.567xw:0.424xh;0.335xw,0.429xh&resize=1400:*',
			rating: 4.9,
			reviewCount: 215,
			specs: [
				{ name: 'Putere', textValue: '600 CP', score: 98 },
				{ name: 'Spațiu Portbagaj', textValue: '565 Litri', score: 85 },
				{ name: 'Tracțiune', textValue: 'Quattro AWD', score: 100 },
				{ name: 'Confort', textValue: 'Pneumatică', score: 90 }
			]
		}
	]);
}
