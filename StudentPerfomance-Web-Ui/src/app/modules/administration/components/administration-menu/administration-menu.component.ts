import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-administration-menu',
  templateUrl: './administration-menu.component.html',
  styleUrl: './administration-menu.component.scss',
})
export class AdministrationMenuComponent {
  @Input({ required: true }) public title!: string;
  @Input({ required: true }) public routerLink!: string[];
}