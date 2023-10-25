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

const students = ref<Student[]>([]);

onBeforeMount(async () => {
  students.value = await fetch("api/student/all").then((res) => res.json());
});
</script>
