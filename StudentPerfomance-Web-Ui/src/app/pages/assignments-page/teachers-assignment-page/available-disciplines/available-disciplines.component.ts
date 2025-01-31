import { Component } from '@angular/core';
import { AvailableDisciplineItemComponent } from './available-discipline-item/available-discipline-item.component';
import { NgForOf, NgIf } from '@angular/common';
import { TeacherAssignmentPageViewmodel } from '../teacher-assignment-page.viewmodel';

@Component({
  selector: 'app-available-disciplines',
  imports: [AvailableDisciplineItemComponent, NgForOf, NgIf],
  templateUrl: './available-disciplines.component.html',
  styleUrl: './available-disciplines.component.scss',
  standalone: true,
})
export class AvailableDisciplinesComponent {
  public constructor(
    protected readonly viewmodel: TeacherAssignmentPageViewmodel,
  ) {}
}
