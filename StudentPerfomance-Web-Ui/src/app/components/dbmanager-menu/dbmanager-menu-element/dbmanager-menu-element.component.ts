import { Component, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-dbmanager-menu-element',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './dbmanager-menu-element.component.html',
  styleUrl: './dbmanager-menu-element.component.scss',
})
export class DbmanagerMenuElementComponent {
  @Input({ required: true }) title!: string;
  @Input({ required: true }) routerLink!: string[];
}
