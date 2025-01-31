import { Component, EventEmitter, Output } from '@angular/core';
import { GreenOutlineButtonComponent } from '../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { YellowOutlineButtonComponent } from '../../../../building-blocks/buttons/yellow-outline-button/yellow-outline-button.component';
import { AssignmentItemComponent } from './assignment-item/assignment-item.component';
import { NgForOf, NgIf } from '@angular/common';
import { TeacherAssignmentPageViewmodel } from '../teacher-assignment-page.viewmodel';

@Component({
  selector: 'app-assignment-journal',
  imports: [
    GreenOutlineButtonComponent,
    YellowOutlineButtonComponent,
    AssignmentItemComponent,
    NgForOf,
    NgIf,
  ],
  templateUrl: './assignment-journal.component.html',
  styleUrl: './assignment-journal.component.scss',
  standalone: true,
})
export class AssignmentJournalComponent {
  @Output() finishClicked: EventEmitter<void> = new EventEmitter();
  public isContentLocked: boolean = true;

  public getCurrentStatus(): string {
    return this.isContentLocked ? 'Только чтение' : 'Режим проставления';
  }

  public constructor(protected viewModel: TeacherAssignmentPageViewmodel) {}
}
