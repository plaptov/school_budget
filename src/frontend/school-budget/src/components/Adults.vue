<template>
  <Suspense>
    <v-container fluid>
      <v-row>
        <v-col>
          <v-btn prepend-icon="$plus" @click="addAdult()"> Добавить </v-btn>
        </v-col>
      </v-row>

      <v-row v-if="editingAdult">
        <v-col>
          <v-card>
            <v-card-title>
              <span class="text-h5">Редактирование</span>
            </v-card-title>
            <v-card-text>
              <v-row>
                <v-col>
                  <v-text-field v-model="editingAdult.name" label="ФИО"></v-text-field>
                </v-col>
                <v-col>
                  <v-text-field v-model="editingAdult.phone" label="Телефон"></v-text-field>
                </v-col>
                <v-col>
                  <v-text-field v-model="editingAdult.comment" label="Комментарий"></v-text-field>
                </v-col>
              </v-row>
              <v-row>
                <v-col>
                  <div style="display:flex; gap: 10px">
                    <v-btn color="primary" @click="saveAdult()">Сохранить</v-btn>
                    <v-btn color="error" @click="editingAdult = null">Отменить</v-btn>
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
                <th class="text-left">Телефон</th>
                <th class="text-left">Комментарий</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="adult in adults" :key="adult.id">
                <td>
                  <v-icon icon="$edit" @click="editingAdult = adult"/>
                  <v-icon icon="$delete" @click="deleteAdult(adult)"/>
                </td>
                <td>{{ adult.id }}</td>
                <td>{{ adult.name }}</td>
                <td>{{ adult.phone }}</td>
                <td>{{ adult.comment }}</td>
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
import { CrudService } from "@/services/crud-service";
import { Adult } from "@/models/adult";

const adults = ref<Adult[]>([]);
const adultsService = new CrudService<Adult>("adult");
const editingAdult = ref<Adult | null>(null);

onBeforeMount(async () => {
  adults.value = await adultsService.getAll();
});

function addAdult(): void {
  editingAdult.value = {
    id: 0,
    name: "",
  }
}

async function saveAdult(): Promise<void> {
  const adult = editingAdult.value!;
  if (adult.id) {
    await adultsService.update(adult);
  } else {
    const newAdult = await adultsService.create(adult);
    adults.value.push(newAdult);
  }
  editingAdult.value = null;
}

async function deleteAdult(adult: Adult): Promise<void> {
  await adultsService.delete(adult.id);
  adults.value = adults.value.filter((s) => s.id !== adult.id);
}
</script>
