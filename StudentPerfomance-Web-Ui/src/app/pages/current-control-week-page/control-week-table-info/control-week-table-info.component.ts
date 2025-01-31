import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AssignmentSession } from '../../../modules/administration/submodules/assignment-sessions/models/assignment-session-interface';
import { BlueButtonComponent } from '../../../building-blocks/buttons/blue-button/blue-button.component';
import { AuthService } from '../../user-page/services/auth.service';
import { NgIf } from '@angular/common';
import { animate, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-control-week-table-info',
  imports: [BlueButtonComponent, NgIf],
  templateUrl: './control-week-table-info.component.html',
  styleUrl: './control-week-table-info.component.scss',
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
export class ControlWeekTableInfoComponent {
  @Input({ required: true }) session: AssignmentSession;
  @Output() createSessionClicked: EventEmitter<void> = new EventEmitter();

  public constructor(protected readonly _auth: AuthService) {}
}
