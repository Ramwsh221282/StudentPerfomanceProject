import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-floating-label-input',
  standalone: true,
  imports: [],
  templateUrl: './floating-label-input.component.html',
  styleUrl: './floating-label-input.component.scss',
})
export class FloatingLabelInputComponent {
  @Input() id: string = '';
  @Input() label: string = '';
  @Input() placeholder: string = '';
  @Input() required: boolean = false;
  @Input() autocomplete: string = 'off';
  @Input() type: string = 'text';
  @Input({ required: true }) value: string;
  @Output() inputChange = new EventEmitter<string>();

  public onInputChange(event: Event) {
    const inputElement = event.target as HTMLInputElement;
    this.value = inputElement.value;
    this.inputChange.emit(this.value);
  }
}
