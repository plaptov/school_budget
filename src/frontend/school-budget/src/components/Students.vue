<template>
  <Suspense>
    <v-container>
      <v-responsive>
        <li v-for="student in students" :key="student.id">
            {{ student.id }} {{ student.name }} {{ student.comment }}
        </li>
      </v-responsive>
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

onBeforeMount(async () => {
  students.value = await studentsService.getAll();
});
</script>
