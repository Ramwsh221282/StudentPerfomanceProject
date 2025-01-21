import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  Output,
  SimpleChanges,
} from '@angular/core';
import { StudentGroup } from '../../services/studentsGroup.interface';
import { Student } from '../../../students/models/student.interface';
import { UserOperationNotificationService } from '../../../../../../shared/services/user-notifications/user-operation-notification-service.service';

@Component({
    selector: 'app-student-group-menu',
    templateUrl: './student-group-menu.component.html',
    styleUrl: './student-group-menu.component.scss',
    standalone: false
})
export class StudentGroupMenuComponent implements OnChanges {
  @Input({ required: true }) group: StudentGroup;
  @Output() moveStudentToOtherGroup: EventEmitter<Student> = new EventEmitter();

  protected tabs: any = [];
  protected activeTabId: number = 1;

  protected studentGroupChangePlanListener: StudentGroup | null;
  protected isChangingStudentGroupPlan: boolean = false;

  protected groupToDeattachPlanFrom: StudentGroup | null;
  protected isDeattachingPlan: boolean = false;

  public constructor(
    private readonly _notificationService: UserOperationNotificationService,
  ) {}

  public ngOnChanges(changes: SimpleChanges): void {
    if (changes['group']) {
      this.updateTabs();
    }
  }

  protected handlePlanDeattachment(group: StudentGroup) {
    group.plan = null!;
    group.activeSemesterNumber = null!;
    this.groupToDeattachPlanFrom = null;
    this.isDeattachingPlan = false;
    this._notificationService.SetMessage = `Откреплён учебный план у группы ${group.name}`;
    this._notificationService.success();
  }

  private updateTabs(): void {
    this.tabs = [
      {
        label: 'Студенты',
        id: 1,
      },
      {
        label: 'Учебный план группы',
        id: 2,
      },
    ];
  }
}
