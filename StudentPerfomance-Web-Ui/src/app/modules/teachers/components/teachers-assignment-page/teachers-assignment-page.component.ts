import { Component } from '@angular/core';

@Component({
  selector: 'app-teachers-assignment-page',
  templateUrl: './teachers-assignment-page.component.html',
  styleUrl: './teachers-assignment-page.component.scss',
})
export class TeachersAssignmentPageComponent {
  protected selectedGroupName: string = 'Выберите группу';
  protected selectedDisciplineName: string = 'Выберите дисциплину';
}
