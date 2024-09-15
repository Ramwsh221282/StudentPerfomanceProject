import { Component, OnInit } from '@angular/core';
import { StudentGroup } from '../services/studentsGroup.interface';
import { ActivatedRoute } from '@angular/router';
import { StudentsListComponent } from './students-list/students-list.component';
import { AddStudentFormComponent } from './add-student-form/add-student-form.component';
import { ManageStudentFormComponent } from './manage-student-form/manage-student-form.component';
import { SearchStudentFormComponent } from './search-student-form/search-student-form.component';
import { RouterLink } from '@angular/router';
import { UserOperationNotificationService } from '../../../shared-services/user-notifications/user-operation-notification-service.service';
import { FacadeStudentService } from './services/facade-student.service';

@Component({
  selector: 'app-group-menu',
  standalone: true,
  imports: [
    StudentsListComponent,
    AddStudentFormComponent,
    ManageStudentFormComponent,
    SearchStudentFormComponent,
    RouterLink,
  ],
  templateUrl: './group-menu.component.html',
  styleUrl: './group-menu.component.scss',
  providers: [FacadeStudentService, UserOperationNotificationService],
})
export class GroupMenuComponent implements OnInit {
  public constructor(
    protected readonly facadeService: FacadeStudentService,
    private readonly _route: ActivatedRoute
  ) {}

  public ngOnInit(): void {
    this.initGroup();
  }

  private initGroup(): void {
    this._route.params.subscribe((params) => {
      const group = { groupName: params['groupName'] } as StudentGroup;
      this.facadeService.setStudentGroup(group);
    });
  }
}
