Create table DocumentResponsible(
	DocumentId int not null,
	UserId int not null,
	IsMain bit not null default(1),
	primary key (DocumentId, UserId)
)
go
alter table DocumentResponsible
add constraint FK_DocumentResponsible_Document foreign key (DocumentId) references Document(DocumentId)
alter table DocumentResponsible
add constraint FK_DocumentResponsible_User foreign key (UserId) references ApplicationUser(ApplicationUserId)
go