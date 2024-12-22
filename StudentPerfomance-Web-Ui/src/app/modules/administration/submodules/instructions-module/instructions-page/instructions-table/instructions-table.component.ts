import { Component } from '@angular/core';
import { NgClass, NgForOf } from '@angular/common';
import { InstructionsDirectionsComponent } from './instructions-directions/instructions-directions.component';
import { InstructionsEduplansComponent } from './instructions-eduplans/instructions-eduplans.component';
import { InstructionsDepartmentsComponent } from './instructions-departments/instructions-departments.component';
import { InstructionsStudentsComponent } from './instructions-students/instructions-students.component';
import { InstructionsAdminsComponent } from './instructions-admins/instructions-admins.component';
import { InstructionsControlWeeksComponent } from './instructions-control-weeks/instructions-control-weeks.component';

@Component({
  selector: 'app-instructions-table',
  standalone: true,
  imports: [
    NgForOf,
    NgClass,
    InstructionsDirectionsComponent,
    InstructionsEduplansComponent,
    InstructionsDepartmentsComponent,
    InstructionsStudentsComponent,
    InstructionsAdminsComponent,
    InstructionsControlWeeksComponent,
  ],
  templateUrl: './instructions-table.component.html',
  styleUrl: './instructions-table.component.scss',
})
export class InstructionsTableComponent {
  protected readonly tabs: any = [
    {
      id: 1,
      label: 'Подготовка системы.\nНаправления подготовки',
    },
    {
      id: 2,
      label: 'Подготовка системы.\nУчебных планы',
    },
    {
      id: 3,
      label: 'Подготовка системы.\nКафедры',
    },
    {
      id: 4,
      label: 'Подготовка системы.\nСтуденческие группы',
    },
    {
      id: 5,
      label: 'Пользователи.\nРегистрация администраторов',
    },
    {
      id: 6,
      label: 'Контрольные недели.\nНачало контрольной недели',
    },
  ];

  protected activeTab: number = 0;
}
