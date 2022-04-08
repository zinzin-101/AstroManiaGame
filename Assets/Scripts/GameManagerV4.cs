using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerV4 : MonoBehaviour
{
    public GameObject plat_prefab;
    public GameObject Break_plat_prefab;
    public GameObject moving_platform_prefab;
    public GameObject quiz_platform_prefab;
    public GameObject[] enemy_prefab;

    public int plat_count = 10;
    public int b_plat_count = 50;
    public int m_plat_count = 25;
    public int q_plat_count = 5;
    private int e_count;

    public Transform target;
    private int plat_gen_coord = 200;
    private int plat_sgen_coord = 180;

    public AudioSource[] break_sound;
    public static bool break_play;

    public void generation_(int count, Vector3 spawnPos, float a, float b, GameObject prefab)
    {
        for (int i = 0; i < count; i++)
        {
            spawnPos.y += Random.Range(a, b);
            spawnPos.x = Random.Range(-5f, 5f);
            Instantiate(prefab, spawnPos, Quaternion.identity);
        }
    }

    public void ex_generation_(int count, Vector3 spawnPos, float a, float b, GameObject[] prefab_, int range)
    {
        int rand_prefab;
        GameObject prefab;

        for (int i = 0; i < count; i++)
        {
            rand_prefab = Random.Range(0, range);
            prefab = prefab_[rand_prefab];
            spawnPos.y += Random.Range(a, b);
            spawnPos.x = Random.Range(-5f, 5f);
            Instantiate(prefab, spawnPos, Quaternion.identity);
        }
    }

    // generating platform after starting
    void Start()
    {
        Vector3 spawn_pos = new Vector3();
        Vector3 s_spawn_pos = new Vector3();
        Vector3 m_spawn_pos = new Vector3();
        Vector3 q_spawn_pos = new Vector3();

        // normal platform
        generation_(plat_count, spawn_pos, 2.15f, 2f, plat_prefab);

        // breakable platform
        generation_(b_plat_count, s_spawn_pos, 10f, 5f, Break_plat_prefab);

        // moving platform
        generation_(m_plat_count, m_spawn_pos, 10f, 6f, moving_platform_prefab);

        // quiz platform
        generation_(q_plat_count, q_spawn_pos, 25f, 10f, quiz_platform_prefab);

        break_play = false;
    }

    void Update()
    {
        // regenerating platform after exceeding certain y coord 
        if (target.position.y > plat_sgen_coord)
        {
            Vector3 spawn_pos = new Vector3();
            Vector3 s_spawn_pos = new Vector3();
            Vector3 m_spawn_pos = new Vector3();
            Vector3 q_spawn_pos = new Vector3();
            Vector3 e_spawn_pos = new Vector3();

            spawn_pos.y = plat_gen_coord;
            s_spawn_pos.y = plat_gen_coord;
            m_spawn_pos.y = plat_gen_coord;
            q_spawn_pos.y = plat_gen_coord;
            e_spawn_pos.y = plat_gen_coord;

            e_count = 6;

            // breakable platform
            generation_(b_plat_count, s_spawn_pos, 10f, 5f, Break_plat_prefab);

            // moving platform
            generation_(m_plat_count, m_spawn_pos, 10f, 6f, moving_platform_prefab);

            // quiz platform
            generation_(q_plat_count, q_spawn_pos, 25f, 10f, quiz_platform_prefab);

            // enemy intensity
            if (target.position.y < 700)
            {
                if (target.position.y >= 360)
                {
                    e_count = 8;
                }
            }
            else if (target.position.y < 900)
            {
                if (target.position.y >= 760)
                {

                    e_count = 10;
                }
            }
            else if (target.position.y >= 960)
            {
                e_count = 12;
            }

            // enemy
            ex_generation_(e_count, e_spawn_pos, 30f, 15f, enemy_prefab, 2);

            plat_gen_coord += 200;
            plat_sgen_coord = plat_gen_coord - 20;
        }

        //breakable platform audio
        if (break_play == true)
        {
            int rand_break = Random.Range(0, 2);
            AudioSource break_effect = break_sound[rand_break];
            break_effect.Play();
            break_play = false;
        }
    }
}