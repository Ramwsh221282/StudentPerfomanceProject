import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-departments-info',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './departments-info.component.html',
  styleUrl: './departments-info.component.scss',
})
export class DepartmentsInfoComponent {}
