import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FloatingLabelInputComponent } from '../../../../../../../../building-blocks/floating-label-input/floating-label-input.component';
import { GreenOutlineButtonComponent } from '../../../../../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { RedOutlineButtonComponent } from '../../../../../../../../building-blocks/buttons/red-outline-button/red-outline-button.component';

@Component({
  selector: 'app-student-status-dropdown',
  standalone: true,
  imports: [
    FloatingLabelInputComponent,
    GreenOutlineButtonComponent,
    RedOutlineButtonComponent,
  ],
  templateUrl: './student-status-dropdown.component.html',
  styleUrl: './student-status-dropdown.component.scss',
})
export class StudentStatusDropdownComponent {
  @Input({ required: true }) visibility: boolean = false;
  @Output() visibilityChange: EventEmitter<boolean> = new EventEmitter();
  @Output() statusSelected: EventEmitter<string> = new EventEmitter();
  protected active: string = 'Активен';
  protected inactive: string = 'Неактивен';

  protected closeDropdown(): void {
    this.visibility = false;
    this.visibilityChange.emit(this.visibility);
  }

  protected selectStatus(status: string): void {
    this.statusSelected.emit(status);
    this.closeDropdown();
  }
}
