import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AssignmentSession } from '../../../../modules/administration/submodules/assignment-sessions/models/assignment-session-interface';
import { NgClass, NgForOf, NgIf } from '@angular/common';
import { ControlWeekGeneralPartComponent } from './control-week-general-part/control-week-general-part.component';
import { ControlWeekGroupsPartComponent } from './control-week-groups-part/control-week-groups-part.component';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-control-week-item',
  imports: [
    NgClass,
    NgForOf,
    ControlWeekGeneralPartComponent,
    NgIf,
    ControlWeekGroupsPartComponent,
  ],
  templateUrl: './control-week-item.component.html',
  styleUrl: './control-week-item.component.scss',
  standalone: true,
  animations: [
    trigger('fadeIn', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateY(-10px)' }),
        animate(
          '300ms ease-out',
          style({ opacity: 1, transform: 'translateY(0)' }),
        ),
      ]),
      transition(':leave', [
        animate(
          '300ms ease-in',
          style({ opacity: 0, transform: 'translateY(-10px)' }),
        ),
      ]),
    ]),
  ],
})
export class ControlWeekItemComponent {
  @Input({ required: true }) session: AssignmentSession;
  @Output() sessionCloseClicked: EventEmitter<AssignmentSession> =
    new EventEmitter();

  public readonly tabs: ControlWeekTab[] = [
    { number: 1, label: 'Информация' },
    { number: 2, label: 'Оценки' },
  ];

  public selectTab(tab: ControlWeekTab, $event: MouseEvent): void {
    $event.stopPropagation();
    this.activeTab = tab.number;
  }

  public activeTab: number = 1;
}

export interface ControlWeekTab {
  number: number;
  label: string;
}
