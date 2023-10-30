<template>
  <Suspense>
    <v-container fluid>
      <v-row>
        <v-col>
          <v-btn prepend-icon="$plus" @click="addStudent()"> Добавить </v-btn>
        </v-col>
      </v-row>

      <v-row v-if="editingStudent">
        <v-col>
          <v-card>
            <v-card-title>
              <span class="text-h5">Редактирование</span>
            </v-card-title>
            <v-card-text>
              <v-row>
                <v-col>
                  <v-text-field v-model="editingStudent.name" label="ФИО"></v-text-field>
                </v-col>
                <v-col>
                  <v-text-field v-model="editingStudent.comment" label="Комментарий"></v-text-field>
                </v-col>
              </v-row>
              <v-row>
                <v-col>
                  <div style="display:flex; gap: 10px">
                    <v-btn color="primary" @click="saveStudent()">Сохранить</v-btn>
                    <v-btn color="error" @click="editingStudent = null">Отменить</v-btn>
                  </div>
                </v-col>
              </v-row>
            </v-card-text>
          </v-card>
        </v-col>
      </v-row>

      <v-row>
        <v-col>
          <v-table>
            <thead>
              <tr>
                <th/>
                <th class="text-left">ID</th>
                <th class="text-left">ФИО</th>
                <th class="text-left">Комментарий</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="student in students" :key="student.id">
                <td>
                  <v-icon icon="$edit" @click="editingStudent = student"/>
                  <v-icon icon="$delete" @click="deleteStudent(student)"/>
                </td>
                <td>{{ student.id }}</td>
                <td>{{ student.name }}</td>
                <td>{{ student.comment }}</td>
              </tr>
            </tbody>
          </v-table>
        </v-col>
      </v-row>
    </v-container>
    <template #fallback> Загрузка... </template>
  </Suspense>
</template>

<script lang="ts" setup>
import { onBeforeMount, ref } from "vue";
import { Student } from "../models/student";
import { CrudService } from "@/services/crud-service";

const students = ref<Student[]>([]);
const studentsService = new CrudService<Student>("student");
const editingStudent = ref<Student | null>(null);

onBeforeMount(async () => {
  students.value = await studentsService.getAll();
});

function addStudent(): void {
  editingStudent.value = {
    id: 0,
    name: "",
    comment: "",
  }
}

async function saveStudent(): Promise<void> {
  const student = editingStudent.value!;
  if (student.id) {
    await studentsService.update(student);
  } else {
    const newStudent = await studentsService.create(student);
    students.value.push(newStudent);
  }
  editingStudent.value = null;
}

async function deleteStudent(student: Student): Promise<void> {
  await studentsService.delete(student.id);
  students.value = students.value.filter((s) => s.id !== student.id);
}
</script>
