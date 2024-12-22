import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { InstructionsTableComponent } from './instructions-table/instructions-table.component';

@Component({
  selector: 'app-instructions-page',
  standalone: true,
  imports: [RouterLink, InstructionsTableComponent],
  templateUrl: './instructions-page.component.html',
  styleUrl: './instructions-page.component.scss',
})
export class InstructionsPageComponent {}
