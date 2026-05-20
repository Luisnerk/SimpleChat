import { Component, inject, signal, TemplateRef, WritableSignal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators, FormsModule } from '@angular/forms';
import { NgbModal, NgbModalRef, NgbSlide } from '@ng-bootstrap/ng-bootstrap';
import { NgSelectModule } from '@ng-select/ng-select';
import { Participant } from '../../_models/participant';
import { catchError, debounce, debounceTime, isEmpty, map, Observable, Subject, switchMap } from 'rxjs';
import { AppUserService } from '../../_services/app-user-service';
import { AsyncPipe } from '@angular/common';
import { User } from '../../_models/user';
import { ChatService } from '../../_services/chat-service';

@Component({
  selector: 'app-chat-create-modal',
  imports: [ReactiveFormsModule, FormsModule, NgSelectModule, AsyncPipe],
  templateUrl: './chat-create-modal.html',
  styleUrl: './chat-create-modal.css',
})
export class ChatCreateModal {
  private modalService = inject(NgbModal);
  private modalRef: NgbModalRef | undefined;
  close: WritableSignal<string> = signal('');
  chatForm: FormGroup = new FormGroup({});
  private searchTerm = new Subject<string>();
  users$: Observable<User[]> | undefined;
  selectedUsers: any[] = [];

  constructor(private fb: FormBuilder, private appUserService: AppUserService, private chatService: ChatService) {}
  
  ngOnInit() {
    this.initializeForm();
    this.users$ = this.searchTerm.pipe(
      debounceTime(400),
      switchMap((term: string) => 
        this.appUserService.getUserList(term).pipe(catchError(() => []))
      )
    );
  }

  initializeForm() {
    this.chatForm = this.fb.group({
      name: ["", Validators.required],
      userIds: [[], Validators.required]
    })
  }

  registerChat() {
    this.chatService.createChat(this.chatForm.value).subscribe({
      next: res => {
        if (res.status == 200){
          this.modalRef?.close();
          this.chatForm.setValue({
            name: "",
            userIds: []
          })
        }
      },
    })
  }

  open(content: TemplateRef<any>) {
    this.modalRef = this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }

  lookFor(value: string) {
    if (value.trim())
      this.searchTerm.next(value.toLowerCase());
  }
}
