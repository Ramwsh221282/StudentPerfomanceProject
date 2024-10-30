import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { StudentGroup } from '../../services/studentsGroup.interface';
import { Student } from '../../../students/models/student.interface';
import { groupStudentsDataService } from './services/group-students-data.service';
import { ActiveOnlyFetchPolicy } from './services/fetch-policies/active-only-fetch-policy';
import { NotActiveFetchPolicy } from './services/fetch-policies/notactive-only-fetch-policy';
import { DefaultFetchPolicy } from './services/fetch-policies/default-fetch-policy';
import { AuthService } from '../../../../../users/services/auth.service';

@Component({
  selector: 'app-students-menu-modal',
  templateUrl: './students-menu-modal.component.html',
  styleUrl: './students-menu-modal.component.scss',
  providers: [groupStudentsDataService],
})
export class StudentsMenuModalComponent implements OnInit {
  @Input({ required: true }) group: StudentGroup;
  @Output() visibility: EventEmitter<boolean> = new EventEmitter();

  protected students: Student[];
  protected activeStudent: Student;

  protected creationModalVisibility: boolean;
  protected deletionModalVisibility: boolean;
  protected filterModalVisibility: boolean;
  protected editModalVisibility: boolean;

  public constructor(
    private readonly _dataService: groupStudentsDataService,
    private readonly _authService: AuthService
  ) {
    this.students = [];
    this.creationModalVisibility = false;
    this.deletionModalVisibility = false;
    this.filterModalVisibility = false;
    this.editModalVisibility = false;
    this.activeStudent = {} as Student;
  }

  public ngOnInit(): void {
    this._dataService.initialize(this.group);
    this.fetchData();
  }

  public close(): void {
    this.visibility.emit(false);
  }

  protected fetchData(): void {
    this._dataService.fetch().subscribe((response) => {
      this.students = response;
    });
  }

  protected setActiveOnlyPolicy(): void {
    this._dataService.setPolicy(
      new ActiveOnlyFetchPolicy(this.group, this._authService)
    );
    this.fetchData();
  }

  protected setNotActiveOnly(): void {
    this._dataService.setPolicy(
      new NotActiveFetchPolicy(this.group, this._authService)
    );
    this.fetchData();
  }

  protected setDefaultPolicy(): void {
    this._dataService.setPolicy(
      new DefaultFetchPolicy(this.group, this._authService)
    );
    this.fetchData();
  }

  protected openCreationModal(): void {
    this.creationModalVisibility = true;
  }

  protected closeCreationModal(value: boolean): void {
    this.fetchData();
    this.creationModalVisibility = value;
  }

  protected openDeletionModal(student: Student): void {
    this.activeStudent = student;
    this.deletionModalVisibility = true;
  }

  protected closeDeletionModal(value: boolean): void {
    this.activeStudent = {} as Student;
    this.fetchData();
    this.deletionModalVisibility = value;
  }

  protected openFilterModal(): void {
    this.filterModalVisibility = true;
  }

  protected closeFilterModal(value: boolean): void {
    this.filterModalVisibility = value;
  }

  protected refreshDataFromFilter(students: Student[]): void {
    this.students = students;
  }

  protected openEditModal(student: Student): void {
    this.activeStudent = student;
    this.editModalVisibility = true;
  }

  protected closeEditModal(value: boolean): void {
    this.activeStudent = {} as Student;
    this.fetchData();
    this.editModalVisibility = value;
  }
}
