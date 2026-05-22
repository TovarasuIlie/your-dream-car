import { Component } from '@angular/core';
import { RouterModule } from "@angular/router";
import { Header } from "../header/header";
import { Footer } from "../footer/footer";
import { ToastComponent } from "../toast/toast.component";

@Component({
  selector: 'app-page-layout',
  imports: [RouterModule, Header, Footer, ToastComponent],
  templateUrl: './page-layout.html',
  styleUrl: './page-layout.css',
})
export class PageLayout {}
