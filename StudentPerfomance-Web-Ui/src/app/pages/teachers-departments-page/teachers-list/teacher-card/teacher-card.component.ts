import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NgIf, NgOptimizedImage } from '@angular/common';
import { Teacher } from '../../../../modules/administration/submodules/teachers/models/teacher.interface';
import { EditIconButtonComponent } from '../../../../building-blocks/buttons/edit-icon-button/edit-icon-button.component';
import { YellowOutlineButtonComponent } from '../../../../building-blocks/buttons/yellow-outline-button/yellow-outline-button.component';
import { RedOutlineButtonComponent } from '../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';
import { RemoveIconButtonComponent } from '../../../../building-blocks/buttons/remove-icon-button/remove-icon-button.component';
import { GreenOutlineButtonComponent } from '../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { AddIconButtonComponent } from '../../../../building-blocks/buttons/add-icon-button/add-icon-button.component';

@Component({
  selector: 'app-teacher-card',
  imports: [
    NgOptimizedImage,
    NgIf,
    EditIconButtonComponent,
    YellowOutlineButtonComponent,
    RedOutlineButtonComponent,
    RemoveIconButtonComponent,
    GreenOutlineButtonComponent,
    AddIconButtonComponent,
  ],
  templateUrl: './teacher-card.component.html',
  styleUrl: './teacher-card.component.scss',
  standalone: true,
})
export class TeacherCardComponent {
  @Input({ required: true }) teacher: Teacher;
  @Output() selectedForRemove: EventEmitter<Teacher> = new EventEmitter();
  @Output() selectedForEdit: EventEmitter<Teacher> = new EventEmitter();
  @Output() selectForRegister: EventEmitter<Teacher> = new EventEmitter();
}
