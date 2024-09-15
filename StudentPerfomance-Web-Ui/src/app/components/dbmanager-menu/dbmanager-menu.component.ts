import { Component } from '@angular/core';
import { DbmanagerMenuElementComponent } from './dbmanager-menu-element/dbmanager-menu-element.component';

@Component({
  selector: 'app-dbmanager-menu',
  standalone: true,
  imports: [DbmanagerMenuElementComponent],
  templateUrl: './dbmanager-menu.component.html',
  styleUrl: './dbmanager-menu.component.scss',
})
export class DbmanagerMenuComponent {}
