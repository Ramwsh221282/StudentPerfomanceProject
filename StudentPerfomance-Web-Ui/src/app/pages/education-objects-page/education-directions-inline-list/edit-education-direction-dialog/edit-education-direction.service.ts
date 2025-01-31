import { Injectable } from '@angular/core';
import { EducationDirection } from '../../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { DirectionPayloadBuilder } from '../../../../modules/administration/submodules/education-directions/models/contracts/direction-payload-builder';
import { Observable } from 'rxjs';
import { BaseHttpService } from '../../../../shared/models/common/base-http/base-http.service';

@Injectable({
  providedIn: 'any',
})
export class EditEducationDirectionService extends BaseHttpService {
  public update(
    initial: EducationDirection,
    updated: EducationDirection,
  ): Observable<EducationDirection> {
    const url = `${this._config.baseApiUri}/api/education-direction`;
    const headers = this.buildHttpHeaders();
    const body = this.buildPayload(initial, updated);
    return this._http.put<EducationDirection>(url, body, {
      headers: headers,
    });
  }

  private buildPayload(
    initial: EducationDirection,
    updated: EducationDirection,
  ): object {
    return {
      initial: DirectionPayloadBuilder(initial),
      updated: DirectionPayloadBuilder(updated),
    };
  }
}
