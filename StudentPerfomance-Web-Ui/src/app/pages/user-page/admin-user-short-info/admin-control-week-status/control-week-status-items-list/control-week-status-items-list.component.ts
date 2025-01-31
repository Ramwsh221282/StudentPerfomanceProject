import { Component, Input } from '@angular/core';
import { AdminControlWeekGroupStatus } from '../admin-control-week-status.component';
import { ControlWeekStatusItemComponent } from './control-week-status-item/control-week-status-item.component';
import { NgForOf } from '@angular/common';

@Component({
  selector: 'app-control-week-status-items-list',
  imports: [ControlWeekStatusItemComponent, NgForOf],
  templateUrl: './control-week-status-items-list.component.html',
  styleUrl: './control-week-status-items-list.component.scss',
  standalone: true,
})
export class ControlWeekStatusItemsListComponent {
  @Input({ required: true }) items: AdminControlWeekGroupStatus[] = [];
}
