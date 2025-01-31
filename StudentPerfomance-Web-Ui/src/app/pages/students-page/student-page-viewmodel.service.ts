import { Injectable } from '@angular/core';
import { StudentGroup } from '../../modules/administration/submodules/student-groups/services/studentsGroup.interface';
import { Student } from '../../modules/administration/submodules/students/models/student.interface';

@Injectable({
  providedIn: 'any',
})
export class StudentPageViewModel {
  public groups: StudentGroup[] = [];
  private _isInitialized: boolean = false;

  public currentGroup: StudentGroup | null = null;

  public setCurrentGroup(group: StudentGroup): void {
    this.currentGroup = group;
  }

  public isGroupCurrent(group: StudentGroup): boolean {
    return this.currentGroup != null && this.currentGroup.id == group.id;
  }

  public attachEducationPlan(
    group: StudentGroup,
    groupWithPlan: StudentGroup,
  ): void {
    const requested = this.getGroup(group);
    if (requested == null) return;
    if (groupWithPlan.plan == null) return;
    if (groupWithPlan.activeSemesterNumber == null) return;
    const index = this.groups.indexOf(requested);
    this.groups[index].plan = { ...groupWithPlan.plan! };
    this.groups[index].activeSemesterNumber =
      groupWithPlan.activeSemesterNumber;
  }

  public detachEducationPlan(group: StudentGroup): void {
    const requested = this.getGroup(group);
    if (requested == null) return;
    const index = this.groups.indexOf(requested);
    this.groups[index].plan = null;
    this.groups[index].activeSemesterNumber = null;
  }

  public initializeGroups(groups: StudentGroup[]): void {
    if (this._isInitialized) return;
    this.groups = groups;
    this._isInitialized = true;
  }

  public removeGroup(group: StudentGroup): void {
    const requested = this.getGroup(group);
    if (requested == null) return;
    const index = this.groups.indexOf(requested);
    if (this.currentGroup != null && this.currentGroup.id == requested.id)
      this.currentGroup = null;
    this.groups.splice(index, 1);
  }

  public appendGroup(group: StudentGroup): void {
    this.groups.push(group);
  }

  public appendStudent(group: StudentGroup, student: Student): void {
    const requestedGroup = this.getGroup(group);
    if (requestedGroup == null) return;
    this.groups[this.groups.indexOf(requestedGroup)].students.push(student);
  }

  public removeStudent(group: StudentGroup, student: Student): void {
    const requestedGroup = this.getGroup(group);
    if (requestedGroup == null) return;
    const requestedStudent = this.getStudent(requestedGroup, student);
    if (requestedStudent == null) return;
    const groupIndex = this.groups.indexOf(requestedGroup);
    const studentIndex =
      this.groups[groupIndex].students.indexOf(requestedStudent);
    this.groups[groupIndex].students.splice(studentIndex, 1);
  }

  private getGroup(requested: StudentGroup): StudentGroup | undefined {
    return this.groups.find((group) => group.id == requested.id);
  }

  private getStudent(
    group: StudentGroup,
    requested: Student,
  ): Student | undefined {
    return group.students.find((student) => student.id == requested.id);
  }
}
