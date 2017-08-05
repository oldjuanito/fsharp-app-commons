﻿namespace MyViewLogic

open Chessie.ErrorHandling
open System.ComponentModel
open FsCommons.Core
open FsCommons.ViewModels
open FsCommons.ViewModels.Base
open System.Windows.Input
open FsCommons.ViewModels.EditableCollections
open Rendition
open ListUpdater
open Navigation

    
type PrimitivesListScreenViewModel(navService: INavigationService)=
    let viewModel = EditableCollectionViewModel<EditableListItemViewModel<PrimitiveDescriptor>>()
    let sendMsgToParent item =
        printf "Call Navigation Parent somehow"
        navService.NavigateTo (NavigationMsg.GoToPrimitiveEdit item)
    let callback (errs,newRend) =
        printfn "Called! %A" newRend
        viewModel.Clear()
        let newItems = newRend |> Seq.map (fun i ->  (EditableListItemViewModel(i, sendMsgToParent)))
        viewModel.AddRange newItems
        printfn "Called! %d" viewModel.Items.Count

    let intialRendtion = Seq.empty
    let updater = Updater(intialRendtion, ListUpdater.updateRenditionFromMsg, callback, ListUpdater.executeAsyncCmds) 
    let msgSender = (updater.SendMsg)
    
    do msgSender Msg.LoadRecords
    member x.ViewModel 
        with get() = viewModel 


