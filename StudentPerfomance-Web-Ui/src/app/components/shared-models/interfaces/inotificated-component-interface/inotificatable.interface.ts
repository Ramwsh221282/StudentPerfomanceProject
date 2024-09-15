import { ModalState } from '../../models/modals/modal-state';

export interface INotificatable {
  successModalState: ModalState;
  failureModalState: ModalState;
}
