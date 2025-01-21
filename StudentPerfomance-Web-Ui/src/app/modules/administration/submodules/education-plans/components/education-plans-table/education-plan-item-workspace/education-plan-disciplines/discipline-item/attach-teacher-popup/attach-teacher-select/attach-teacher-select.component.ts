import { Component, EventEmitter, Input, Output } from '@angular/core';
import { DropdownListComponent } from '../../../../../../../../../../../building-blocks/dropdown-list/dropdown-list.component';

@Component({
    selector: 'app-attach-teacher-select',
    imports: [DropdownListComponent],
    templateUrl: './attach-teacher-select.component.html',
    styleUrl: './attach-teacher-select.component.scss'
})
export class AttachTeacherSelectComponent {
  @Output() visibilityChanged: EventEmitter<void> = new EventEmitter();
  @Input({ required: true }) departmentNames: string[];
  @Output() selectedDepartmentName: EventEmitter<string> = new EventEmitter();
}
