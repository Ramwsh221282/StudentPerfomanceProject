import {
  AfterViewInit,
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Teacher } from '../../../../../dbmanager-departments-view/department-menu/models/teacher.interface';
import { SemesterPlan } from '../../../models/semester-plan.interface';
import { DepartmentsFetchService } from '../../../../../dbmanager-departments-view/services/departments-fetch.service';
import { Department } from '../../../../../dbmanager-departments-view/services/departments.interface';
import { SearchTeacherService } from '../../../../../dbmanager-departments-view/department-menu/services/search-teacher.service';

@Component({
  selector: 'app-semester-plan-card-info',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './semester-plan-card-info.component.html',
  styleUrl: './semester-plan-card-info.component.scss',
  providers: [BsModalService, DepartmentsFetchService, SearchTeacherService],
})
export class SemesterPlanCardInfoComponent implements OnInit, AfterViewInit {
  @ViewChild('template') template: TemplateRef<any>;
  @Output() modalDisabled: EventEmitter<boolean>;
  @Output() selectedTeacher: EventEmitter<Teacher>;
  @Input({ required: true }) cardPlanInfo: SemesterPlan;
  protected searchedResults: Teacher[];
  protected fetchedDepartments: Department[];

  public constructor(
    private readonly _modalService: BsModalService,
    private readonly _fetchService: DepartmentsFetchService,
    private readonly _searchService: SearchTeacherService
  ) {
    this.selectedTeacher = new EventEmitter();
    this.modalDisabled = new EventEmitter();
    this._modalService.onHide.subscribe(() => this.closeModal());
  }

  public ngOnInit(): void {
    this.searchedResults = [];
    this.fetchedDepartments = [];
    this.fetchDepartments();
  }

  public ngAfterViewInit(): void {
    this._modalService.show(this.template);
  }

  protected closeModal(): void {
    this._modalService.hide();
    this.modalDisabled.emit(false);
  }

  protected selectDepartmentOption(departmentName: string): void {
    if (departmentName == null || departmentName == undefined) return;
    this.cardPlanInfo.attachedTeacherDepartmentName = departmentName;
    this.fetchTeachersOfDepartment(departmentName);
  }

  protected selectTeacherOption(selectedOption: string): void {
    if (selectedOption == null || selectedOption == undefined) return;
    const namesArray = selectedOption.split(' ');
    this.cardPlanInfo.attachedTeacherSurname = namesArray[0];
    this.cardPlanInfo.attachedTeacherName = namesArray[1];
    this.cardPlanInfo.attachedTeacherThirdname = namesArray[2];
  }

  protected assignTeacher(): void {
    if (!this.isSelectionValid()) return;
    const result = {
      name: this.cardPlanInfo.attachedTeacherName,
      surname: this.cardPlanInfo.attachedTeacherSurname,
      thirdname: this.cardPlanInfo.attachedTeacherThirdname,
      departmentName: this.cardPlanInfo.attachedTeacherDepartmentName,
    } as Teacher;
    this.closeModal();
    this.selectedTeacher.emit(result);
  }

  private fetchTeachersOfDepartment(departmentName: string): void {
    const department = { name: departmentName } as Department;
    const factory = this._searchService.createRequestParamFactory(department);
    this._searchService.search(factory).subscribe((response) => {
      this.searchedResults = response;
    });
  }

  private fetchDepartments(): void {
    this._fetchService.fetchAll().subscribe((response) => {
      this.fetchedDepartments = response;
    });
  }

  private isSelectionValid(): boolean {
    if (
      this.cardPlanInfo.attachedTeacherName == null ||
      this.cardPlanInfo.attachedTeacherSurname == null ||
      this.cardPlanInfo.attachedTeacherThirdname == null ||
      this.cardPlanInfo.attachedTeacherDepartmentName == null
    ) {
      return false;
    }
    return true;
  }
}
