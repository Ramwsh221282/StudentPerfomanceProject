import { Component, EventEmitter, Input, Output } from '@angular/core';
import { StudentGroup } from '../../../../modules/administration/submodules/student-groups/services/studentsGroup.interface';
import { NgClass, NgIf, NgOptimizedImage } from '@angular/common';
import { EditIconButtonComponent } from '../../../../building-blocks/buttons/edit-icon-button/edit-icon-button.component';
import { RemoveIconButtonComponent } from '../../../../building-blocks/buttons/remove-icon-button/remove-icon-button.component';

@Component({
  selector: 'app-student-group-card',
  imports: [
    NgOptimizedImage,
    NgClass,
    EditIconButtonComponent,
    RemoveIconButtonComponent,
    NgIf,
  ],
  templateUrl: './student-group-card.component.html',
  styleUrl: './student-group-card.component.scss',
  standalone: true,
})
export class StudentGroupCardComponent {
  @Input({ required: true }) group: StudentGroup;
  @Input({ required: true }) isSelected: boolean = false;
  @Output() selectCard: EventEmitter<StudentGroup> = new EventEmitter();
  @Output() selectCardForEdit: EventEmitter<StudentGroup> = new EventEmitter();
  @Output() selectCardForRemove: EventEmitter<StudentGroup> =
    new EventEmitter();

  public handleSelection($event: MouseEvent): void {
    $event.stopPropagation();
    this.selectCard.emit(this.group);
  }
}
