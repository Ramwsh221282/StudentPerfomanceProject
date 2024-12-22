import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-instructions-students',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './instructions-students.component.html',
  styleUrl: './instructions-students.component.scss',
})
export class InstructionsStudentsComponent {}
