import { Component, OnInit } from '@angular/core';
import {tap} from "rxjs/operators";
import {MatDialogRef} from "@angular/material/dialog";

@Component({
  selector: 'app-shared-modal-yes-no',
  templateUrl: './shared-modal-yes-no.component.html',
  styleUrls: ['./shared-modal-yes-no.component.scss']
})
export class SharedModalYesNoComponent implements OnInit {

  constructor(
    private dialogRef: MatDialogRef<SharedModalYesNoComponent>) { }

  ngOnInit(): void {
  }

  close() {
    this.dialogRef.close(false);
  }

  save() {
    this.dialogRef.close(true);
  }
}
