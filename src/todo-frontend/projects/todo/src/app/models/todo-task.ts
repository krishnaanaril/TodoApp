export type TodoTask = {
    id: number;
    description: string;
    status: TodoTaskStatus;
    createdTime: Date;
    updatedTime: Date;
}

export enum TodoTaskStatus {
    New,
    InProgress,
    Completed,
    Deleted
}