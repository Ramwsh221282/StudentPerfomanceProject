import { Component, OnInit } from '@angular/core';
import { TeacherAssignmentInfoSessionService } from './teacher-assignment-session-info.service';
import { AssignmentSessionInfo } from './assignment-session-into';

@Component({
  selector: 'app-teacher-assignment-session-info',
  templateUrl: './teacher-assignment-session-info.component.html',
  styleUrl: './teacher-assignment-session-info.component.scss',
  providers: [TeacherAssignmentSessionInfoComponent],
})
export class TeacherAssignmentSessionInfoComponent implements OnInit {
  protected info: AssignmentSessionInfo;

  public constructor(
    private readonly _service: TeacherAssignmentInfoSessionService
  ) {
    this.info = {} as AssignmentSessionInfo;
  }

  public ngOnInit(): void {
    this._service.getInfo().subscribe((response) => {
      this.info = response;
    });
  }
}
