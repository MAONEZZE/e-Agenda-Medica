import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogConfig } from '@angular/material/dialog';
import { DialogExcluirComponent } from 'src/app/shared/componentes/dialog-excluir/dialog-excluir.component';

@Component({
  selector: 'app-dialog-visualizacao',
  templateUrl: './dialog-visualizacao.component.html',
  styleUrls: ['./dialog-visualizacao.component.scss']
})
export class DialogVisualizacaoComponent {
  constructor(
    public dialogRef: MatDialogRef<DialogExcluirComponent>, 
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit(): void {
    this.abrirDialog();
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  abrirDialog(){
    const dialogConfig = new MatDialogConfig();

    dialogConfig.width = '300px';
    dialogConfig.maxHeight = '637px';

    this.dialogRef.updateSize(dialogConfig.width, dialogConfig.maxHeight);
  } 
}
