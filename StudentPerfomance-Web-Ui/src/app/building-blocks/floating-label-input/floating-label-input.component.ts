import {
  ChangeDetectorRef,
  Component,
  EventEmitter,
  Input,
  Output,
} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-floating-label-input',
  imports: [FormsModule, NgIf],
  templateUrl: './floating-label-input.component.html',
  styleUrl: './floating-label-input.component.scss',
  standalone: true,
})
export class FloatingLabelInputComponent {
  private _showContent: boolean = false;
  @Input() id: string = '';
  @Input() label: string = '';
  @Input() placeholder: string = '';
  @Input() required: boolean = false;
  @Input() autocomplete: string = 'off';
  @Input() type: string = 'text';
  @Input() isReadonly: boolean = false;
  @Input({ required: true }) value: string;
  @Output() inputChange = new EventEmitter<string>();

  @Input()
  get showContent(): boolean {
    return this._showContent;
  }

  set showContent(value: boolean) {
    this._showContent = value;
    this._cdr.markForCheck();
  }

  constructor(private _cdr: ChangeDetectorRef) {}

  public onInputChange(event: Event) {
    const inputElement = event.target as HTMLInputElement;
    this.value = inputElement.value;
    this.inputChange.emit(this.value);
  }
}
