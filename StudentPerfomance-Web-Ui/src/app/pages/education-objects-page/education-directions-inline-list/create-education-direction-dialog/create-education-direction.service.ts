import { Injectable } from '@angular/core';
import { EducationDirection } from '../../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { DirectionPayloadBuilder } from '../../../../modules/administration/submodules/education-directions/models/contracts/direction-payload-builder';
import { Observable } from 'rxjs';
import { BaseHttpService } from '../../../../shared/models/common/base-http/base-http.service';

@Injectable({
  providedIn: 'any',
})
export class CreateEducationDirectionService extends BaseHttpService {
  public create(direction: EducationDirection): Observable<EducationDirection> {
    const url = `${this._config.baseApiUri}/api/education-direction`;
    const headers = this.buildHttpHeaders();
    const body = this.buildPayload(direction);
    return this._http.post<EducationDirection>(url, body, {
      headers: headers,
    });
  }

  private buildPayload(direction: EducationDirection): object {
    return {
      command: DirectionPayloadBuilder(direction),
    };
  }
}
