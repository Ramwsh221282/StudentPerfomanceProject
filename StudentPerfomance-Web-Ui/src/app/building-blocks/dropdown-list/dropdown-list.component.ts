import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NgForOf } from '@angular/common';
import { RedOutlineButtonComponent } from '../buttons/red-outline-button/red-outline-button.component';

@Component({
    selector: 'app-dropdown-list',
    imports: [NgForOf, RedOutlineButtonComponent],
    templateUrl: './dropdown-list.component.html',
    styleUrl: './dropdown-list.component.scss'
})
export class DropdownListComponent {
  @Input({ required: true }) items: string[];
  @Output() visibilityChanged: EventEmitter<boolean> = new EventEmitter();
  @Output() itemSelected: EventEmitter<string> = new EventEmitter();

  public handleItemSelection(event: Event, selectedItem: string): void {
    event.stopPropagation();
    this.itemSelected.emit(selectedItem);
    this.visibilityChanged.emit(false);
  }
}
