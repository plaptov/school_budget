<template>
  <Suspense>
    <v-responsive>
      <v-table>
        <thead>
          <tr>
            <th class="text-left">ID</th>
            <th class="text-left">ФИО</th>
            <th class="text-left">Комментарий</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="student in students" :key="student.id">
            <td>{{ student.id }}</td>
            <td>{{ student.name }}</td>
            <td>{{ student.comment }}</td>
          </tr>
        </tbody>
      </v-table>
    </v-responsive>
    <template #fallback> Загрузка... </template>
  </Suspense>
</template>

<script lang="ts" setup>
import { onBeforeMount, ref } from "vue";
import { Student } from "../models/student";
import { CrudService } from "@/services/crud-service";

const students = ref<Student[]>([]);
const studentsService = new CrudService<Student>("student");

onBeforeMount(async () => {
  students.value = await studentsService.getAll();
});
</script>
