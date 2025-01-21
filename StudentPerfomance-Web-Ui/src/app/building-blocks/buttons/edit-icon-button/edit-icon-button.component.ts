import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NgOptimizedImage } from '@angular/common';

@Component({
  selector: 'app-edit-icon-button',
  imports: [NgOptimizedImage],
  templateUrl: './edit-icon-button.component.html',
  styleUrl: './edit-icon-button.component.scss',
  standalone: true,
})
export class EditIconButtonComponent {
  @Output() onClicked = new EventEmitter<void>();
  @Input({ required: true }) paramHeight: number = 1;
  @Input({ required: true }) paramWidth: number = 1;

  public onClick($event: Event): void {
    $event.stopPropagation();
    this.onClicked.emit();
  }
}
