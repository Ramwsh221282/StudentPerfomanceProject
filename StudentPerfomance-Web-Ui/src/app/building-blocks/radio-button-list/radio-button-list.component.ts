import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NgForOf } from '@angular/common';

@Component({
  selector: 'app-radio-button-list',
  imports: [NgForOf],
  templateUrl: './radio-button-list.component.html',
  styleUrl: './radio-button-list.component.scss',
  standalone: true,
})
export class RadioButtonListComponent {
  @Output() valueChecked: EventEmitter<string> = new EventEmitter();
  @Input({ required: true }) values: IRadioButtonValue[] = [];

  public onValueSelect(value: IRadioButtonValue, $event: MouseEvent): void {
    $event.stopPropagation();
    this.valueChecked.emit(value.value);
  }
}

export interface IRadioButtonValue {
  value: string;
  isChecked: boolean;
}
