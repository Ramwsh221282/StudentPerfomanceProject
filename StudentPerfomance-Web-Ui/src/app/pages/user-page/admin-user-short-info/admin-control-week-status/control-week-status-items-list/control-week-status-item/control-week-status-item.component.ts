import { Component, Input } from '@angular/core';
import { AdminControlWeekGroupStatus } from '../../admin-control-week-status.component';
import { NgClass, NgForOf, NgIf } from '@angular/common';
import {
  animate,
  state,
  style,
  transition,
  trigger,
} from '@angular/animations';

@Component({
  selector: 'app-control-week-status-item',
  imports: [NgIf, NgClass, NgForOf],
  templateUrl: './control-week-status-item.component.html',
  styleUrl: './control-week-status-item.component.scss',
  standalone: true,
  animations: [
    trigger('expandCollapse', [
      state(
        'false',
        style({
          height: '0',
          opacity: 0,
          overflow: 'hidden',
        }),
      ),
      state(
        'true',
        style({
          height: '*',
          opacity: 1,
          overflow: 'visible',
        }),
      ),
      transition('false => true', [animate('300ms ease-out')]),
      transition('true => false', [animate('300ms ease-in')]),
    ]),
  ],
})
export class ControlWeekStatusItemComponent {
  @Input({ required: true }) item: AdminControlWeekGroupStatus;
  public isInfoShown: boolean = false;

  public showInfo($event: MouseEvent): void {
    $event.stopPropagation();
    this.isInfoShown = !this.isInfoShown;
  }
}
