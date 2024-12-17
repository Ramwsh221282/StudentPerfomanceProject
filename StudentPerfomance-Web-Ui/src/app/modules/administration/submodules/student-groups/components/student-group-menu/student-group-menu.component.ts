import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { StudentGroup } from '../../services/studentsGroup.interface';

@Component({
  selector: 'app-student-group-menu',
  templateUrl: './student-group-menu.component.html',
  styleUrl: './student-group-menu.component.scss',
})
export class StudentGroupMenuComponent implements OnChanges {
  @Input({ required: true }) group: StudentGroup;

  protected tabs: any = [];
  protected activeTabId: number = 1;

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['group']) {
      this.updateTabs();
    }
  }

  private updateTabs(): void {
    this.tabs = [
      {
        label: 'Студенты',
        id: 1,
      },
      {
        label: 'Учебный план группы',
        id: 2,
      },
    ];
  }
}
