import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AssignmentSession } from '../../../../../modules/administration/submodules/assignment-sessions/models/assignment-session-interface';
import { GreenOutlineButtonComponent } from '../../../../../building-blocks/buttons/green-outline-button/green-outline-button.component';
import { AuthService } from '../../../../user-page/services/auth.service';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-control-week-general-part',
  imports: [GreenOutlineButtonComponent, NgIf],
  templateUrl: './control-week-general-part.component.html',
  styleUrl: './control-week-general-part.component.scss',
  standalone: true,
})
export class ControlWeekGeneralPartComponent {
  @Input({ required: true }) session: AssignmentSession;
  @Output() assignmentSessionCloseClicked: EventEmitter<AssignmentSession> =
    new EventEmitter();

  public constructor(protected _auth: AuthService) {}
}
